#!/bin/bash
#chmod u+rwx configs/install.sh

cd .configs
bower install
nuget install -OutputDirectory ../.packages/
cd ..

mkdir -p Assets/Packages/StrangeIoC
rm -r Assets/Packages/StrangeIoC
cp -r .packages/strangeioc/StrangeIoC/scripts/strange/ Assets/Packages/StrangeIoC

echo "Done"
