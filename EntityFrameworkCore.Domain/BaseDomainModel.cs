namespace EntityFrameworkCore.Domain;

public abstract class BaseDomainModel
{
    public int Id { get; set; }

    public DateTime CreatedDate { get; set; }

    //This would be a int in a real env with it being a foreign key from a Users table
    //This would also not be nullable in a real world scenario.
    public string? CreateBy { get; set; }

    public DateTime ModifiedDate { get; set; }

    //This would be a int in a real env with it being a foreign key from a Users table
    //This would also not be nullable in a real world scenario.
    public string? ModifiedBy { get; set; }
}