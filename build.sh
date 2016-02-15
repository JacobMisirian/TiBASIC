#!/bin/bash

xbuild src/TIBASIC.sln
sudo cp src/TIBASIC/bin/Debug/TIBASIC.exe /usr/bin/TIBASIC.exe
sudo cp tibasic.sh /usr/bin/tibasic
sudo chmod +x /usr/bin/tibasic
