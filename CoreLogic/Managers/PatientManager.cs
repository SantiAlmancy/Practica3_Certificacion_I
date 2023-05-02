using UPB.CoreLogic.Models;
using System.Xml.Serialization;

namespace UPB.CoreLogic.Managers;

public class PatientManager
{
    private string filePath;
    public PatientManager(string _filePath)
    {
        filePath = _filePath;
    }

    //CREATE C
    
    public Patient Create(string name, string lastname, int ci)
    {
        if (ci < 0)
        {
            throw new Exception("INVALID CI");
        }
        // List of all blood groups
        string[] bloodGroups = new string[] 
        {"A-",
        "A+",
        "B+",
        "B-",
        "AB+",
        "AB-",
        "O+",
        "O-"};

        // Randomly choosing a blood group
        Random rand = new Random();
        int index = rand.Next(8);

        Patient createdPatient = new Patient(name, lastname, bloodGroups[index], ci);
        List<Patient> patients = GetAll();
        patients.Add(createdPatient);
        SavePatients(patients);
        return createdPatient;
    }

    //  Save patient

    public void SavePatients(List<Patient> patients)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Patient>));

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, patients);
        }
    }


    // READ R
    // GET BY ID

    public Patient GetById(int ci)
    {
        if (ci < 0)
        {
            throw new Exception("INVALID CI");
        }

        List<Patient> patients = GetAll();
        Patient patient = patients.Find(p => p.CI == ci);

        if (patient == null)
        {
            throw new Exception("Patient not found");
        }

        return patient;
    }

    // GET ALL

    public List<Patient> GetAll()
    {
        if (!File.Exists(filePath))
        {
            return new List<Patient>();
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Patient>));

        using (StreamReader reader = new StreamReader(filePath))
        {
            return (List<Patient>)serializer.Deserialize(reader);
        }
    }

    // UPDATE U

    public Patient Update(int ci, string name, string lastName)
    {
        if (ci < 0)
        {
            throw new Exception("INVALID CI");
        }

        List<Patient> patients = GetAll();

        if (patients.Find(p => p.CI == ci) == null)
        {
            throw new Exception("Patient not found");
        }
        else
        {
            Patient patientToUpdate = patients.Find(p => p.CI == ci);

            patientToUpdate.Name = name;
            patientToUpdate.LastName = lastName;

            SavePatients(patients);

            return patientToUpdate;
        }
    }

}