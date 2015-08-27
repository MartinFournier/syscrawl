#!/bin/bash
#chmod u+rwx configs/install.sh

#Folders
CONFIG_FOLDER=".configs"
PACKAGES_FOLDER=".3rd"
PACKAGES_OUTPUT="Assets/Vendor"
SEP="########################"
echo "Fetching the packages from bower & nuget"
echo $SEP
#Fetch the packages
cd $CONFIG_FOLDER
bower install
nuget install -OutputDirectory ../$PACKAGES_FOLDER/
cd ..

echo "Cleanup"
echo $SEP
#Cleanup & Prep
rm -r $PACKAGES_OUTPUT
mkdir -p $PACKAGES_OUTPUT

echo "Copying packages"
echo $SEP
#StrangeIoC (only the framework, excluding examples)
cp -r $PACKAGES_FOLDER/strangeioc/StrangeIoC/scripts/strange/ $PACKAGES_OUTPUT/StrangeIoC
# NUnit, NGenerics
cp -r $PACKAGES_FOLDER/N* $PACKAGES_OUTPUT/

echo ""
echo "Install Complete."
