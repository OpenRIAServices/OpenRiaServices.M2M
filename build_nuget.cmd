
@set OPTIONS=-build -verbosity normal -Prop Configuration=Release -OutputDirectory nupkgs -IncludeReferencedProjects

@if not exist nupkgs (mkdir nupkgs)
@NuGet.exe pack OpenRiaServices.M2M.LinkTable\OpenRiaServices.M2M.LinkTable.csproj %OPTIONS%
@NuGet.exe pack OpenRiaServices.M2M.Silverlight\OpenRiaServices.M2M.Silverlight.csproj %OPTIONS%
@NuGet.exe pack OpenRiaServices.M2M\OpenRiaServices.M2M.csproj %OPTIONS%
