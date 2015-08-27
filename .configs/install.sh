#!/bin/bash
#chmod u+rwx configs/install.sh

CONFIG_FOLDER=".configs"
PACKAGES_FOLDER=".packages"

PACKAGES_OUTPUT="Assets/Packages"

cd $CONFIG_FOLDER
bower install
nuget install -OutputDirectory ../$PACKAGES_FOLDER/
cd ..



#mkdir -p $PACKAGES_OUTPUT/StrangeIoC
rm -r $PACKAGES_OUTPUT
mkdir -p $PACKAGES_OUTPUT
cp -r $PACKAGES_FOLDER/strangeioc/StrangeIoC/scripts/strange/ $PACKAGES_OUTPUT/StrangeIoC

echo "Done"
