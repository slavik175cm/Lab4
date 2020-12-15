# Lab4
DataAccessLayer(dll)
Работатет в общем случае с базой данных
Передаются такие параметры как строка подключения, имя хранимой процедуры, некоторые параметры выборки
Подключена к ServiceLayer

ServiceLayer(dll)
Осуществляет заполнение моделей, используя DataAccessLayer
Возращет коллекцию комплексных моделей
Подключена к DataManager

SupportServices(dll) (работает в общем случае)
Содержит FileUtils - для работы с файлами(реализует интерфейс IFileTransferService)
XMLGeneratorService - для генерации XML файла на основе переданной коллекции моделей
Подключена к DataManager

ConfigManager(dll) (сделана в лаб3)
Нужена для получения настроек из кофигурационных файлов(заполняет переданную модель)
(подробно описана в readme https://github.com/slavik175cm/Watcher)
Подключена к DataManager и FileManager

DataManager
Получает свою конфигурацию(ConfigManager).
Затем Batch'ами достает информацию из базы данных (ServiceLayer)
Каждый Batch конвертится в XML и формирует файл (SupportServices)
Затем перемещает этот файл в указанную дерикторию(для FileManager'а) (IFileTransferService)

FileManager(сделан в лаб2)
Вин служба мониторящая файлы и папки(!). 



