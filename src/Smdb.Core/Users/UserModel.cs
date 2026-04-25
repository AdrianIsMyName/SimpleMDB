namespace Smdb.Core.Users; 
public class User
{ 
  public int Id { get; set; } 
  public string Name { get; set; } 
  public string Email { get; set; } 
  public string Password { get; set; } 
  public string Salt { get; set; } 
  public string Role { get; set; } 
 
  public User(int id, string name, string email, string password, string salt, string role) 
  { 
    Id = id; 
    Name = name; 
    Email = email; 
    Password = password; 
    Salt = salt; 
    Role = role; 
  } 
 
  public override string ToString() 
  { 
    return $"User[Id={Id}, Name={Name}, Email={Email}, Password={Password}, string Salt={Salt}, string Role={Role}]"; 
  }
}