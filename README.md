# MITIE-Dot-Net
A C# based, .NET Core Standard Library wrapper for the MITIE Information Extraction Library 0.4.

Supports the entire MITIE API as defined in mitie.h

The original MITIE project is here: https://github.com/mit-nlp/MITIE

Model files can be found here: http://sourceforge.net/projects/mitie/files/binaries/
You can use the trained ner model from here - ner_model.dat.

See sample XUnit test class file MarshaledWrapperTest for usage.

**Notes**:

I've included a 64 bit compiled version of MITIE - mitie.dll against the latest MITIE commit
832cbd6 (Feb 10, 2019). Your consuming client will have to be compiled against a target of x64, or if 
using AnyCPU, make sure it runs on a 64 bit OS.

If running the Unit Tests or the TestConsoleApp, make sure to drop the trained ner model file into
the root of the solution (where sample1.txt is located).

Official Nuget is at -> https://www.nuget.org/packages/MitieNetCore/1.0.2 and is a 64bit build.
 

# TODO:

* Finish adding OO classes and sample usage to encapsulate the Mitie API.
* Add more unit tests
* Add Source Link for nuget package

# Common Issues:
