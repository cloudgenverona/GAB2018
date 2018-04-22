#!/usr/bin/env bash

echo "Building tests project..."
msbuild ../GabDemo.Tests/GabDemo.Tests.csproj /p:Configuration=Release

echo "Logging in..."
appcenter login --token "f1d882f81531704405a1e62ea3612e8063491608"

echo "Launching tests..."

appcenter test run uitest --app "matteot/GAB-2018" \
    --devices "matteot/top-selling" \
    --app-path "../Droid/bin/Release/matteotumiati.GabDemo.apk" \
    --test-series "ui-tests" \
    --locale "en_US" \
    --build-dir "../GabDemo.Tests/bin/Release/" \
    --async

echo "Current branch is $APPCENTER_BRANCH"