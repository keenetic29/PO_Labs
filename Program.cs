using System;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using System.IO.Compression;

namespace PO1
{
  class Program
  {
    private static int menuValue;
    public static void MenuOptions()
    {
      Console.WriteLine("1. Вывести информацию в консоль о логических дисках, именах, метке тома, размере и типе файловой системы.");
      Console.WriteLine("2. Работа с файлами (класс File, FileInfo, FileStream и другие).");
      Console.WriteLine("3. Работа с форматом JSON.");
      Console.WriteLine("4. Работа с форматом XML.");
      Console.WriteLine("5. Создание zip архива, добавление туда файла, определение размера архива.");

      Console.Write("Выберите нужный пункт меню: ");
      menuValue = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine();
    }

    static void Main(string[] args)
    {
     
      while (true)
      {
        MenuOptions();
        switch (menuValue)
        {
          case 1:
            PCInformation.GetInfo();
            break;
          case 2:
            FileManager.FileManagerMain();
            break;
          case 3:
            WorkWithJson.JsonMain();
            break;
          case 4:
            WorkWithXML.XmlMain();
            break;
          case 5:
            Zip.ZipMain();
            break;


          case 0:
            return;

          default:
            break;
        }
      }
      

    }
  }

  class Student
  {
    public string Name { get; set; }
    public int Age { get; set; }
  }

  static class PCInformation
  {

    public static void GetInfo()
    {
      DriveInfo[] drives = DriveInfo.GetDrives();

      foreach (DriveInfo drive in drives)
      {
        Console.WriteLine($"Название: {drive.Name}");
        Console.WriteLine($"Тип: {drive.DriveType}");
        if (drive.IsReady)
        {
          Console.WriteLine($"Объем диска: {drive.TotalSize}");
          Console.WriteLine($"Свободное пространство: {drive.TotalFreeSpace}");
          Console.WriteLine($"Метка: {drive.VolumeLabel}");
        }
        Console.WriteLine();
      }
    }

  }

  class FileManager
  {
    private static int fileValue;
    public static void FileOption()
    {
      Console.WriteLine("2 ПУНКТ МЕНЮ - Работа с файлами");
      Console.WriteLine("1. Создать файл.");
      Console.WriteLine("2. Записать в файл строку, введённую пользователем.");
      Console.WriteLine("3. Прочитать файл в консоль.");
      Console.WriteLine("4. Удалить файл.");
      Console.WriteLine("0. Выход в меню.");
      Console.Write("Выберите нужный пункт меню: ");
      fileValue = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine();
    }

    public static void FileManagerMain()
    {
      while (true)
      {
        FileOption();
        switch (fileValue)
        {
          case 1:
            CreateFile();
            break;

          case 2:
            WriteInFile();
            break;

          case 3:
            ReadFile();
            break;

          case 4:
            DeleteFile();
            break;

          case 0:
            return;

          default:
            Console.WriteLine("Неверный выбор.");
            break;
        }
      }
      
    }

    public static void CreateFile()
    {
      Console.Write("Введите имя файла: ");
      string fileName = Console.ReadLine();
      File.Create(fileName).Close();
      Console.WriteLine($"Файл {fileName} создан.");
    }

    public static void WriteInFile()
    {
      Console.Write("Введите имя файла: ");
      string fileNameToWrite = Console.ReadLine();
      Console.Write("Введите строку для записи в файл: ");
      string inputString = Console.ReadLine();
      File.WriteAllText(fileNameToWrite, inputString);
      Console.WriteLine($"Строка записана в файл {fileNameToWrite}.");
    }

    public static void ReadFile()
    {
      Console.Write("Введите имя файла: ");
      string fileNameToRead = Console.ReadLine();
      Console.WriteLine("Содержимое файла:");
      Console.WriteLine(File.ReadAllText(fileNameToRead));
    }

    public static void DeleteFile()
    {
      Console.Write("Введите имя файла: ");
      string fileName = Console.ReadLine();
      if (File.Exists(fileName))
      {
        File.Delete(fileName);
        Console.WriteLine($"Файл {fileName} удален.");
      }
      else
      {
        Console.WriteLine($"Файл {fileName} не существует. По этой причине он не может быть удален.");
      }
    }
  }


  class WorkWithJson : FileManager
  {
    public static int jsonValue;
    public static void JsonMenu()
    {
      Console.WriteLine("3 ПУНКТ МЕНЮ - Работа с форматом JSON");
      Console.WriteLine("1. Создать файл формата JSON.");
      Console.WriteLine("2. Создать новый объект. Выполнить сериализацию объекта в формате JSON и записать в файл.");
      Console.WriteLine("3. Прочитать файл в консоль.");
      Console.WriteLine("4. Удалить файл.");
      Console.WriteLine("0. Выйти в меню.");
      Console.Write("Выберите нужный пункт: ");
      jsonValue = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine();
    }
    public static void JsonMain()
    {
      while (true)
      {
        JsonMenu();
        switch (jsonValue)
        {
          case 1:
            JsonCreate();
            break;
          case 2:
            JsonCreateObject();
            break;
          case 3:
            ReadJson();
            break;
          case 4:
            DeleteJson();
            break;

          case 0:
            return;

          default:
            Console.WriteLine("Неверный выбор.");
            break;
        }
      }
      
    }

    public static void JsonCreate()
    {
      Student student = new Student { Name = "Default", Age = 0 };
      Console.Write("Введите имя json файла: ");
      string fileName =  Console.ReadLine();

      var options = new JsonSerializerOptions { WriteIndented = true };
      string json = JsonSerializer.Serialize(student, options);
      File.WriteAllText(fileName, json);
      Console.WriteLine($"Файл {fileName} создан.");

    }

    public static void JsonCreateObject()
    {

      Console.WriteLine("Создание объекта класса Студент. Введите необходимые данные.");
      Console.WriteLine("Введите имя студента:");
      string name = Console.ReadLine();
      Console.WriteLine("Введите возраст студента:");
      int age = int.Parse(Console.ReadLine());

      Student student = new Student { Name = name, Age = age};
      Console.Write("Введите имя json файла: ");
      string fileName = Console.ReadLine();

      var options = new JsonSerializerOptions { WriteIndented = true };
      string json = JsonSerializer.Serialize(student, options);
      File.WriteAllText(fileName, json);
      Console.WriteLine($"Файл {fileName} обновлён.");
      return;
    }

    public static void ReadJson()
    {
      Console.Write("Введите имя json файла: ");
      string fileName = Console.ReadLine();
      if (File.Exists(fileName))
      {
        string data = File.ReadAllText(fileName);
        Student student = JsonSerializer.Deserialize<Student>(data);
        Console.WriteLine($"Имя: {student.Name}; Возраст: {student.Age}");
      }
      else
      {
        Console.WriteLine($"Файл {fileName} не существует. По этой причине он не может быть прочитан.");
      }
    }

    public static void DeleteJson()
    {
      FileManager.DeleteFile();
    }
  }

  class WorkWithXML : FileManager
  {
    public static int xmlValue;
    public static void XmlMenu()
    {
      Console.WriteLine("4 ПУНКТ МЕНЮ - Работа с форматом XML");
      Console.WriteLine("1. Создать файл в формате XML из редактора.");
      Console.WriteLine("2. Записать в файл новые данные из консоли.");
      Console.WriteLine("3. Прочитать файл в консоль.");
      Console.WriteLine("4. Удалить файл.");
      Console.WriteLine("0. Выйти в меню.");
      Console.Write("Выберите нужный пункт: ");
      xmlValue = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine();
    }
    public static void XmlMain()
    {
      while (true)
      {
        XmlMenu();
        switch (xmlValue)
        {
          case 1:
            XmlCreate();
            break;
          case 2:
            WriteInXml();
            break;
          case 3:
            ReadXml();
            break;
          case 4:
            DeleteXml();
            break;

          case 0:
            return;

          default:
            Console.WriteLine("Неверный выбор.");
            break;
        }
      }
    }

    public static void XmlCreate()
    {
      XDocument xdoc = new XDocument(
        new XElement("student",
            new XElement("Name", "Default"),
            new XElement("Age", 0)));

      Console.Write("Введите имя xml файла: ");
      string fileName = Console.ReadLine();

      xdoc.Save(fileName);
      Console.WriteLine($"Файл {fileName} создан.");

    }

    public static void WriteInXml()
    {
      Console.WriteLine("Введите имя студента:");
      string name = Console.ReadLine();
      Console.WriteLine("Введите возраст студента:");
      int age = int.Parse(Console.ReadLine());
      XDocument xdoc = new XDocument(
        new XElement("student",
            new XElement("Name", name),
            new XElement("Age", age)));

      Console.Write("Введите имя xml файла: ");
      string fileName = Console.ReadLine();
      if (File.Exists(fileName))
      {
        xdoc.Save(fileName);
        Console.WriteLine($"Файл {fileName} обновлён.");
      }
      else
      {
        Console.WriteLine($"Файл {fileName} не существует.");
      }
      
    }

    public static void ReadXml()
    {
      Console.Write("Введите имя xml файла: ");
      string fileName = Console.ReadLine();
      if (File.Exists(fileName))
      {
        XDocument xdoc = XDocument.Load(fileName);
        // получаем корневой узел
        XElement? student = xdoc.Element("student");
        if (student != null)
        {
            XElement? name = student.Element("Name");
            XElement? age = student.Element("Age");
            Console.WriteLine($"Имя: {name?.Value}; Возраст: {age?.Value}");
        }
      }
      else
      {
        Console.WriteLine($"Файл {fileName} не существует.");
      }
    }

    public static void DeleteXml()
    {
      FileManager.DeleteFile();
    }

  }

  class Zip
  {
    public static int zipValue;
    public static void ZipMenu()
    {
      Console.WriteLine("5 ПУНКТ МЕНЮ - Работа с ZIP");
      Console.WriteLine("1. Создать архив в формате zip. Добавить файл, выбранный пользователем, в архив.");
      Console.WriteLine("2. Разархивировать файл и вывести данныне о нём.");
      Console.WriteLine("3. Удалить файл и архив.");
      Console.WriteLine("0. Выйти в меню.");
      Console.Write("Выберите нужный пункт: ");
      zipValue = Convert.ToInt32(Console.ReadLine());
      Console.WriteLine();
    }

    public static void ZipMain()
    {
      while (true)
      {
        ZipMenu();
        switch (zipValue)
        {
          case 1:
            CreateZipAndAddFile();
            break;
          case 2:
            ReadFileInZip();
            break;
          case 3:
            DeleteFileAndZip();
            break;


          case 0:
            return;

          default:
            Console.WriteLine("Неверный выбор.");
            break;
        }
      }
    }

    public static void CreateZipAndAddFile()
    {
      Console.Write("Введите имя файла: ");
      string fileName = Console.ReadLine();

      string fullPath = Path.GetFullPath(fileName);
      int indexOfChar = fullPath.LastIndexOf('\\');

      string zipPath = fullPath.Substring(0, indexOfChar+1);

      zipPath = zipPath + "result.zip";

      using (var fileStream = new FileStream(zipPath, FileMode.Create))
      using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
      {
        archive.CreateEntryFromFile(fullPath, fileName);
      }
      Console.WriteLine("Архив успешно создан. Файл добавлен в архив.");
    }

    public static void ReadFileInZip()
    {
      string archiveName = "result.zip"; 

      string fullPath = Path.GetFullPath(archiveName);
      int indexOfChar = fullPath.LastIndexOf('\\');

      string zipPath = fullPath.Substring(0, indexOfChar + 1);



      string archiveFilePath = fullPath; //полный путь к файлу архива
      string targetFolder = zipPath;//путь к папке в которую будет распакован архиа
      bool overriteFiles = true; //перезаписать файлы, если они уже есть в папке нахначения
      ZipFile.ExtractToDirectory(archiveFilePath, targetFolder, overriteFiles);
    }

    public static void DeleteFileAndZip()
    {
      Console.Write("Введите имя файла: ");
      string fileName = Console.ReadLine();
      if (File.Exists(fileName))
      {
        File.Delete(fileName);
        Console.WriteLine($"Файл {fileName} успешно удален.");
      }
      else
      {
        Console.WriteLine($"Файл {fileName} не существует.");
      }

      Console.Write("Введите имя архива: ");
      string zipName = Console.ReadLine();
      if (File.Exists(zipName))
      {
        File.Delete(zipName);
        Console.WriteLine($"Архив {zipName} успешно удален.");
      }
      else
      {
        Console.WriteLine($"Архив {zipName} не существует.");
      }
    }

  }
  
}
