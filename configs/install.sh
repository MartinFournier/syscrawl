#!/bin/bash
#chmod u+rwx configs/install.sh
cd configs
bower install
nuget install -OutputDirectory ../packages/
cd ..
echo "Done"
