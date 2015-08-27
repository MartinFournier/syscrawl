#!/bin/bash
#chmod u+rwx configs/install.sh
PROJECT="syscrawl"

cd configs
bower install
nuget install -OutputDirectory ../packages/
cd ..

mkdir -p $PROJECT/Assets/Packages/StrangeIoC
rm -r $PROJECT/Assets/Packages/StrangeIoC
cp -r packages/strangeioc/StrangeIoC/scripts/strange/ $PROJECT/Assets/Packages/StrangeIoC

echo "Done"
