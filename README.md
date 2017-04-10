# Driverstore Permissions Repair
Program to repair driverstore permissions when Windows screws up during an update.

### The Problem
Windows 10 Creators Update comes with the usual range of problems that Microsoft has become known for recently. One of these is the "black screen with circles" before login. This is caused by an out of date primary display driver which can be uninstalled while in safe mode.  
  
Unfortunately, if you encounter this issue while an update was being installed (given Windows 10's approach of changing settings and installing updates without asking the user first, this is fairly likely) your driver store permissions will become corrupted. This means that when you go to install an up-to-date display driver you will get the very helpful "`this operation requires an interactive window station`" error message. This very relevant, helpful and informative error message is caused by corrupt permissions of the Windows driver store files.

### What This Program Does
This program will asyncronously perform the following commands to reset the permissions of any unreadable file within the driver store:  
```
takeown /F <file>
icacls <file> /reset
icacls <file> /grant Administrators:f
icacls <file> /setowner SYSTEM
```

### Known Affected Devices
- Dell XPS 9550 (15" Laptop)

### Notes
- The program has been implemented in C# to take advantage of performance (I had ~50,000 corrupt file permissions), parallelism and because I like it more than powershell/cmd.
- You **must** be an administrator to run this program.
- This program will have the side-affect of granting any administrator full-control access to these files, however, given any administrator can just grant themselves these permissions anyway it should not cause much issue. I do not know of any way to set the owner back to system without granting Administrators these rights.

### License
MIT
