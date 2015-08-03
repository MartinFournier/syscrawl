#!/bin/bash
#chmod u+rwx configs/install.sh
cd configs
bower install
nuget install -OutputDirectory ../packages/
cd ..
rm -r syscrawl/Assets/Packages/StrangeIoC
cp -r packages/strangeioc/StrangeIoC/scripts/strange/ syscrawl/Assets/Packages/StrangeIoC
echo "Done"
