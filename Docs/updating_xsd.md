# Обновление XSD-схем

1. Скачать архив схем с сайта https://честныйзнак.рф/business/projects/medicines/documents/for_developers/ — Схемы и форматы для разработчиков учётных систем_v1.35 (7z, 23 КБ)
2. Распаковать в папку с проектом MdlpApiClient.Xsd
3. Пересобрать проект MdlpApiClient.Xsd, не добавляя его в солюшн: dotnet build MdlpApiClient.Xsd.csproj. Перегенерируется MdlpApiClient.Xsd.cs.
4. Обновить single-file версию (в студии выполнить Run custom tool на файле MdlpApiClient.Merged\MdlpClientSingleFile.tt)
5. Обновить информацию в файле проекта для публикации nuget-пакета (версия схемы, дата и пр.), увеличить номер версий Version и FileVersion, но не AssemblyVersion
6. Собрать nuget-пакет в папке проекта MdlpApiClient: dotnet pack MdlpApiClient.csproj
