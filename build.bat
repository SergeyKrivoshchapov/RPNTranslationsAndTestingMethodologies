@echo off
set "PROJECT_ROOT=%~dp0"

cd exprToPostfix\dll && go build -buildmode=c-shared -o "%PROJECT_ROOT%RpnManualTests\RpnManualTests\bin\Debug\net10.0\exprToPostfix.dll" . && cd /d "%PROJECT_ROOT%"
cd postfixCalculation\dll && go build -buildmode=c-shared -o "%PROJECT_ROOT%RpnManualTests\RpnManualTests\bin\Debug\net10.0\postfixCalculation.dll" . && cd /d "%PROJECT_ROOT%"


echo Done.