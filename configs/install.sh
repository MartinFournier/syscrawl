#!/bin/bash
#chmod u+rwx configs/install.sh
cd configs
bower install
nuget install -OutputDirectory ../packages/
cd ..
rm -r unity/Assets/Packages/StrangeIoC
cp -r packages/strangeioc/StrangeIoC/scripts/strange/ unity/Assets/Packages/StrangeIoC
echo "Done"
