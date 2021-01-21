# keylogger

## Dec. 3, 2017

A crappy basic keylogger

"Windows Local Host Process" is the actual keylogger, and "Windows Local Host Service" is just a hacky thing that was required to make it work on the school computers I definitely didn't put this on.

To use: download the exe, and run it once. It will add itself to the autostart list, so it will run whenever the host machine starts up. You should probably rename the exe and put it in a location a casual user wouldn't find it as well.
It will store it's logs in C:\Users\username\AppData\Windows (screenshots are stored in the \data subdirectory as .bmp files renamed to .dat)

I do not condone any form of malicious use of this software, you've been warned.
Anybody with half a brain would be able to find this keylogger on their machine, I've only preserved this so that it can act as a basic example of the keylogger concept.

### *0.0.6 (01/21/21)*
----------------------
- Additional functionality to take screenshots of all monitors every 5 minutes.

### *0.0.5 (01/13/21)*
-----------------------
- Fix hardcoded path for log files

### *0.0.4 (09/17/20)*
----------------------
- Quick patch to create hardcoded log path

### *0.0.3 (08/10/20)*
----------------------
- Cleanup and fixes for emergency use

### *0.0.2 (12/05/17)*
-----------------------
- Add autorun service

### *0.0.1 (12/03/17)*
-----------------------
- Initial version
