namespace UPB.PracticeThree.Models;

public class Patient
{
    public String Name {get; set;}
    public String LastName {get; set;}
    public String BloodGroup {get; set;}
    public int CI {get; set;}
    
    public Patient(String Name, String LastName, String BloodGroup, int CI)
    {
        this.Name = Name;
        this.LastName = LastName;
        this.BloodGroup = BloodGroup;
        this.CI = CI;
    }
}