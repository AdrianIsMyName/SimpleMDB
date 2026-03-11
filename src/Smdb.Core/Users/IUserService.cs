namespace Smdb.Core.Users; 
 
using Shared.Http; 
 
public interface IUserService 
{ 
  public Task<Result<PagedResult<User>>> ReadUsers(int page, int size); 
  public Task<Result<User>> CreateUser(User user); 
  public Task<Result<User>> ReadUser(int id); 
  public Task<Result<User>> UpdateUser(int id, User newData); 
  public Task<Result<User>> DeleteUser(int id); 
}