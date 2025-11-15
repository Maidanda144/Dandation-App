namespace AttendanceBackend.Models
{
    public class Student
    {
        public string Name { get; set; } = "";
        public string Class { get; set; } = "";
        public string Status { get; set; } = ""; // "Present", "Absent" ou ""
        public string ParentNumber { get; set; } = ""; // +233...
    }
}