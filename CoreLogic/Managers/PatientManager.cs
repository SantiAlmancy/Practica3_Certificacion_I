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
}