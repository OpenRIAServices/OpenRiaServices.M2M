
@set OPTIONS=-build -verbosity normal -Prop Configuration=Release -OutputDirectory nupkgs

@if not exist nupkgs (mkdir nupkgs)
@.\.nuget\NuGet.exe pack RIAServices.M2M.LinkTable\RIAServices.M2M.LinkTable.csproj %OPTIONS%
@.\.nuget\NuGet.exe pack RIAServices.M2M.Silverlight\RIAServices.M2M.Silverlight.csproj %OPTIONS%
@.\.nuget\NuGet.exe pack RIAServices.M2M\RIAServices.M2M.csproj %OPTIONS%
