; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{8E91A2FE-5373-45BF-A13D-53EA72020ABA}
AppName=text-rpg
AppVersion=1.5
;AppVerName=text-rpg 1.5
AppPublisher=Sarkhanas
AppPublisherURL=https://github.com/Sarkhanas/text-rpg
AppSupportURL=https://github.com/Sarkhanas/text-rpg
AppUpdatesURL=https://github.com/Sarkhanas/text-rpg
DefaultDirName={pf}\text-rpg
DefaultGroupName=text-rpg
AllowNoIcons=yes
OutputDir=C:\Users\Daniil\Desktop\text-rpg
OutputBaseFilename=setup
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Daniil\Desktop\text-rpg\ConsoleApp2\bin\Debug\netcoreapp3.1\ConsoleApp2.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Daniil\Desktop\text-rpg\ConsoleApp2\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\text-rpg"; Filename: "{app}\ConsoleApp2.exe"
Name: "{commondesktop}\text-rpg"; Filename: "{app}\ConsoleApp2.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\ConsoleApp2.exe"; Description: "{cm:LaunchProgram,text-rpg}"; Flags: nowait postinstall skipifsilent

