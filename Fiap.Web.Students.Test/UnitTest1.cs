namespace Fiap.Web.Students.Test;

public class UnitTest1
{
    [Fact]
    public void CheckAgeOfMajority_ShouldReturnTrueIfMajority()
    {
        //Arrange
        var birthdate = new DateTime(2000, 1, 1);
        var today = DateTime.Now;
        var ageOfMajority = today.Year - birthdate.Year;
        
        if (birthdate > today.AddYears(-ageOfMajority)) --ageOfMajority;
        
        //Act
        var isAgeOfMajority = ageOfMajority >= 18;
        
        //Assert
        Assert.True(isAgeOfMajority);
    }
    
    [Fact]
    public void CheckAgeOfMajority_ShouldReturnTrueIfMinority()
    {
        //Arrange
        var birthdate = new DateTime(2020, 1, 1);
        var today = DateTime.Now;
        var ageOfMajority = today.Year - birthdate.Year;
        
        if (birthdate > today.AddYears(-ageOfMajority)) --ageOfMajority;
        
        //Act
        var isAgeOfMinority = ageOfMajority < 18;
        
        //Assert
        Assert.True(isAgeOfMinority);
    }
}