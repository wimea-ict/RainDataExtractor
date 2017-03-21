# RainFallDataExtractor
Extracts daily rainfall from WIMEA-ICT AWS data
USAGE: RainDataExtractor.exe inputfile outputfile
An input file must have atleast 1 line.
The line must contain a date as the first 19 bytes and a valid P0_LST60 tag
e.g 2016-11-27 03:15:43 TXT=makg2-gnd P0=0xAF P0_LST60=0  UP=0x65028 V_A1=1.20  V_A2=2.65 

#To run on Linux
1. Install mono. apt-get install mono-runtime (just to run executables) or apt-get install mono-complete (to compile .cs files)
2. Run the .exe using mono RainDataExtractor.exe inputfile outputfile
