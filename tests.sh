#!/usr/bin/env bash

echo
echo ==================================================================
echo  Building and running Identity Server
echo ==================================================================
echo

docker build -f ./src/TokenServer/Dockerfile -t tokenserver .

docker run --rm -d -p 8002:8002 --name token-server tokenserver

echo
echo ==================================================================
echo  Building and running tests
echo ==================================================================
echo

docker build -f ./tests/Vendor/Vendor.API.Tests/Dockerfile -t vendor_api_tests .

docker run --rm --net "host" --name vendor-api-tests vendor_api_tests

echo
echo ==================================================================
echo  Tidy up - stop Identity Server
echo ==================================================================
echo

docker stop my-identity-server