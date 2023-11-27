# GAPResource
1) Скачайте архив с проектом
2) Подгрузите в базу [путь к проекту]\GAPResource\db\test_gap_resource.mdf (либо можно работать непосредственно с mdf файлом)
3) В файле [путь к проекту]\GAPResource\GAPResource\CustomConf\conf.cfg измените ConnectionString на свой
4) Запустите команду dotnet restore [путь к проекту]\GAPResource\GAPResource.sln
5) Запустите команду dotnet restore dotnet publish -c Release -o e:\GAPResource(путь к сборке) [путь к проекту]\GAPResource\GAPResource.sln  --no-restore
6) Запустите GAPResource.exe
7) Перейдите по указаному Url-у из "Now listening on"