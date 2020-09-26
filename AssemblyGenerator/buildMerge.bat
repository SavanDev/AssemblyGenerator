@echo off

if not exist bin\Merged (
    mkdir bin\Merged
)

Thirdparty\ILMerge\ILMerge.exe bin\Release\AssemblyGenerator.exe /lib:bin\Release /out:bin\Merged\AssemblyGenerator.exe Newtonsoft.Json.dll /target:exe