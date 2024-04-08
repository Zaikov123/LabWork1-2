using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using LabKozl;
using Newtonsoft.Json;

public class DataController
{
    private static DataController _instance;
    private static readonly object _lock = new object();

    private List<Department> _departments;
    private List<Car> _cars;
    private List<Driver> _drivers;
    private List<DTP> _dtps;
    private List<Member> _members;

    private DataController()
    {
        _departments = new List<Department>();
        _cars = new List<Car>();
        _drivers = new List<Driver>();
        _dtps = new List<DTP>();
    }

    public static DataController GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new DataController();
                }
            }
        }
        return _instance;
    }

    #region JSON WORK
    private List<T> LoadDataFromJson<T>(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
            }
            else
            {
                Console.WriteLine("File doesn't exist");
                return new List<T>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data from {filePath}: {ex.Message}");
            return new List<T>();
        }
    }

    private void SaveDataToFile<T>(string filePath, List<T> collection)
    {
        try
        {
            string json = JsonConvert.SerializeObject(collection, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data to {filePath}: {ex.Message}");
        }
    }
    #endregion
    #region Save and Load data
    public void LoadData()
    {
        _departments = LoadDataFromJson<Department>("departments.json");
        _cars = LoadDataFromJson<Car>("cars.json");
        _drivers = LoadDataFromJson<Driver>("drivers.json");
        _dtps = LoadDataFromJson<DTP>("dtps.json");
        _members = LoadDataFromJson<Member>("members.json");
    }

    public void SaveData()
    {
        SaveDataToFile("departments.json", _departments);
        SaveDataToFile("cars.json", _cars);
        SaveDataToFile("drivers.json", _drivers);
        SaveDataToFile("dtps.json", _dtps);
        SaveDataToFile("members.json", _members);
    }
    #endregion
    #region ADDDATA
    public void AddDepartment(Department department)
    {
        _departments.Add(department);
    }

    public void AddCar(Car car)
    {
        _cars.Add(car);
    }

    public void AddDriver(Driver driver)
    {
        _drivers.Add(driver);
    }

    public void AddDTP(DTP dtp)
    {
        _dtps.Add(dtp);
    }
    #endregion
    #region REMOVEDATA
    public void RemoveCar(Car car)
    {
        _cars.Remove(car);
    }
    #endregion
    #region DISLPAY
    public List<Department> GetDepartments()
    {
        return _departments;
    }

    public List<Car> GetCars()
    {
        return _cars;
    }

    public List<Driver> GetDrivers()
    {
        return _drivers;
    }

    public List<DTP> GetDTPs()
    {
        return _dtps;
    }
    #endregion
    #region Terminal
    public void ShowMainMenu()
    {
        Console.WriteLine("1. Show Data\n2.Add DTP\n3. Delete DTP\n4. Exit");
        string option = Console.ReadLine();
        if (option != string.Empty) GetMethodByOption(option);
    }

    private void GetMethodByOption(string option)
    {
        switch(option)
        {
            case "1": DisplayData(); break;
            case "2": AddDTP(); break; 
            case "3": DeleteDTP(); break;
            case "4": SaveData(); break;
            default: Console.WriteLine("Invalid Option"); break;
        }
    }

    private void DisplayData()
    {
        Console.WriteLine("Departments:");
        foreach (var item in _departments)
        {
            Console.WriteLine($"Id: {item.Id}\tName: {item.Name}");
        }
        Console.WriteLine("Cars:");
        foreach (var item in _cars)
        {
            Console.WriteLine($"Id: {item.Id}\tName: {item.Name}");
        }
        Console.WriteLine("Drivers:");
        foreach (var item in _drivers)
        {
            Console.WriteLine($"Id: {item.Id}\tName: {item.FullName}\tCetrificate: {item.Certificate}");
        }
        Console.WriteLine("DTPs:");
        foreach (var item in _dtps)
        {
            Console.WriteLine($"Id: {item.Id}\tDTP cause: {item.DTPCause}\tDTP date: {item.DateDTP}");
        }
    }
    private void AddDTP()
    {
        try
        {
            Console.WriteLine("Enter the date and time of the accident (yyyy-MM-dd):");
            DateTime dateDTP = DateTime.ParseExact(Console.ReadLine(), "yyyy-MM-dd", null);

            Console.WriteLine("Enter the place of the accident:");
            string place = Console.ReadLine();

            Console.WriteLine("Enter the casualty rate:");
            uint casualtyRate = uint.Parse(Console.ReadLine());

            Console.WriteLine("Enter the act number:");
            ulong actNumber = ulong.Parse(Console.ReadLine());

            Console.WriteLine("Enter the cause of the accident:");
            string dtpCause = Console.ReadLine();
            DTP newDTP = new DTP
            {
                Id = _dtps.Any() ? _dtps.Last().Id + 1 : 1,
                DateDTP = dateDTP,
                Plase = place,
                СasualtyRate = casualtyRate,
                ActNumber = actNumber,
                DTPCause = dtpCause
            };

            GetInstance().AddDTP(newDTP);
            Console.WriteLine("DTP added successfully.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding DTP: {ex.Message}");
        }
        SaveDataToFile("dtps.json", _dtps);
    }
    private void DeleteDTP()
    {
        try
        {
            Console.WriteLine("Select DTP by Id to delete:");
            
            foreach (var dtp in GetInstance().GetDTPs())
            {
                Console.WriteLine($"Id: {dtp.Id}\nDTP cause: {dtp.DTPCause}\nDTP date: {dtp.DateDTP}");
            }

            
            Console.WriteLine("Enter the Id of the DTP you want to delete:");
            int idToDelete = int.Parse(Console.ReadLine());

            
            DTP dtpToDelete = GetInstance().GetDTPs().FirstOrDefault(d => d.Id == idToDelete);
            if (dtpToDelete != null)
            {
                _dtps.Remove(dtpToDelete);
                Console.WriteLine("DTP deleted successfully.");
            }
            else
            {
                Console.WriteLine("DTP with specified Id not found.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting DTP: {ex.Message}");
        }
        SaveDataToFile("dtps.json", _dtps);
    }

    #endregion
}
