using System.Diagnostics;

class TemplateGenerator {
    static void Main(string[] args) {
        string inputFile = null;
        string dllName = null;
        bool compileFlag = false;
        int i = 0;
        if(args.Length == 0) {
            PrintUsage();
            return;
        }
        while(i < args.Length) {
            string arg = args[i];
            if(arg.StartsWith("-c=")) {
                compileFlag = true;
                dllName = arg.Substring(3);
            }
            else if(arg == "-i") {
                if(i + 1 < args.Length)
                    inputFile = args[++i];
                else {
                    Console.WriteLine("Missing argument for -i.");
                    PrintUsage();
                    return;
                }
            }
            else {
                Console.WriteLine($"Unknown option: {arg}");
                PrintUsage();
                return;
            }

            i++;
        }
        if(string.IsNullOrEmpty(inputFile)) {
            Console.WriteLine("Input file is required.");
            PrintUsage();
            return;
        }
        if(compileFlag) {
            string tempPath = Path.Combine(Path.GetTempPath(), $"compiled_{Guid.NewGuid()}.c");
            GenerateTemplate(inputFile, tempPath);
            CompileTemplate(tempPath, dllName);
        }
        else {
            GenerateTemplate(inputFile, $"{Path.GetFileNameWithoutExtension(inputFile)}.c");
        }
    }

    static void PrintUsage() {
        string exeName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);

        Console.WriteLine($"Usage:");
        Console.WriteLine($"{exeName} -i input.txt -c=output.dll");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  -i    Specify the input file containing export names.");
        Console.WriteLine("  -c=<name> Compile the generated template into a DLL with the specified name.");
        Console.WriteLine();
        Console.WriteLine("Example:");
        Console.WriteLine($"  {exeName} -i input.txt -c=output.dll");
    }

    static void GenerateTemplate(string inputFile, string outputFile) {
        try {
            if(!File.Exists(inputFile)) {
                Console.WriteLine($"Input file {inputFile} not found.");
                return;
            }

            string templateContent = @"
#include <windows.h>
#include <stdio.h>

#define DLL_EXPORT __declspec(dllexport)

void ShowMessage(const char* msg)
{
    MessageBoxA(NULL, msg, ""Notification"", MB_OK | MB_ICONINFORMATION);
}

#define EXPORT_FUNCTION(name) \
    DLL_EXPORT void name() { \
        char msg[256]; \
        snprintf(msg, sizeof(msg), #name "" called.""); \
        ShowMessage(msg); \
    }
";

            var writer = new StreamWriter(outputFile);
            writer.Write(templateContent);
            foreach(string exportName in File.ReadLines(inputFile))
                if(!string.IsNullOrEmpty(exportName.Trim()))
                    writer.WriteLine($"EXPORT_FUNCTION({exportName.Trim()})");
            Console.WriteLine($"Template generation completed! Output written to {outputFile}");
        } catch(Exception ex) {
            Console.WriteLine($"Failed to generate template: {ex.Message}");
        }
    }

    static void CompileTemplate(string inputFile, string dllName) {
        try {
            var compileProcess = new Process {
                StartInfo = new ProcessStartInfo {
                    FileName = "gcc",
                    Arguments = $"-o {dllName}.dll -shared -s -O3 {inputFile}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            compileProcess.Start();
            string output = compileProcess.StandardOutput.ReadToEnd();
            string errors = compileProcess.StandardError.ReadToEnd();
            compileProcess.WaitForExit();
            if(compileProcess.ExitCode == 0)
                Console.WriteLine($"Compilation successful. DLL file: {dllName}.dll");
            else
                Console.WriteLine($"Compilation failed with errors:\n{errors}");
        } catch(Exception ex) {
            Console.WriteLine($"Compilation encountered an error: {ex.Message}");
        }
    }
}
