export SONAR_TOKEN=6d3bc56f598aeb045c82bcbaff4d8aac36221524
dotnet sonarscanner begin \
  /o:objectgraph \
  /k:objectgraph_SameGameBlazor \
  /d:sonar.host.url=https://sonarcloud.io
dotnet build
dotnet sonarscanner end