# Комплекс программ распределенного кеширования
## Состав комплекса
Комплекс состоит из двух служб, выполненных в виде консольных приложений с возможностью их установки как служб Windows.
1. *CacheSystemService* - служба, реализующая функционал распределенной системы кеширования.
1. *RegisterService* - служба регистрации экземпляров CacheSystemService.

## Порядок запуска
При запуске *CacheSystemService* происходит регистрация его хоста в службе *RegisterService*. Поэтому **перед запуском *CacheSystemService* необходимо запустить *RegisterService***.

1. Запускаем один экземпляр *RegisterService*.
1. Запускаем необходимое количество экземпляров *CacheSystemService*.

##Настройка *RegisterService*
Программа регистрирует все запущенные сервисы *CacheSystemService*. Это необходимо для общения экземпляров *CacheSystemService* между собой.
***.config*** файл содержит следующие настройки:
```xml
 <appSettings>
    <add key="hostUrl" value="http://localhost:9090"/>
  </appSettings>
```

*hostUrl* - адрес хоста, на котором развернута служба.
Программа написана с использованием [TopShelf](http://topshelf-project.com/ "Topshelf"), поэтому может быть развернута как служба Windows при помощи команд:
```
RegisterService install
RegisterService uninstall
```
##Настройка *CacheSystemService*
Данные для сохранения в кеше представлены классом Employee.
```csharp
 public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
```
Во всех экземплярах кеша данные доступны по уникальному ключу типа string, уникальность ключа должна быть обеспечена на уровне всего кеша, а не только его отдельных экземпляров.
Поиск осуществляется до первого найденного экземпляра данных по ключу.

Программа содержит следующие настройки:
```xml
 <appSettings>
    <add key="hostUrl" value="http://localhost:8080"/>
    <add key="registerUrl" value="http://localhost:9090"/>
  </appSettings>
```
- *hostUrl* - адрес хоста, на котором развернут экземпляр системы кеширования.
- *registerUrl* - адрес хоста службы *RegisterService*

Программа написана с использованием [TopShelf](http://topshelf-project.com/ "Topshelf"), поэтому может быть развернута как служба Windows при помощи команд:
```
CacheSystemService install
CacheSystemService uninstall
```
##Системные требования
NET Framework 4.7.2








 

