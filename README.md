`dllexp-x64/dllexp.exe` using this, i exported the all exports to `win-x64/raw.txt`
<br>
from there i used `exporter.exe` to make a fake `wer.dll` with all the exports as msgbox to see what i used
<br>
i then took the realavent exports and made `fakewer/fakewer.c`, compiled and viola notepad opened
<br>
*requires gcc in path*


![image](https://github.com/user-attachments/assets/9357e3b2-da71-4331-afb6-92b52d7b6dd1)

fakewer.dll (wer.dll)
![image](https://github.com/user-attachments/assets/b5850b9e-d26a-4043-8264-a3de157513fb)
![image](https://github.com/user-attachments/assets/83371475-03ec-4168-ab0f-d845cbf627b8)
