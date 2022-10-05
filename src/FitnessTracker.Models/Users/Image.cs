namespace FitnessTracker.Models.Users;

public class Image
{
     public int Id { get; set; }
     public byte[] Bytes { get; set; }
     public string Name { get; set; }
     public string FileExtension { get; set; }
}
