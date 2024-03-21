using PasswordManager.Helpers;
using PasswordManager.Models;
using SQLite;

namespace PasswordManager.Data;
public class SQLiteHelper
{
    SQLiteAsyncConnection Database;
    public SQLiteHelper()
    {
    }
    async Task Init()
    {
        if (Database is not null) return;
        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await Database.CreateTableAsync<User>();
        await Database.CreateTableAsync<Cuenta>();
        await Database.CreateTableAsync<Categoria>();
        await Database.InsertAsync(new Categoria() { CategoriaId = 1, Nombre = "Sitios Web"});
        await Database.InsertAsync(new Categoria() { CategoriaId = 2, Nombre = "Aplicaciones"});
    }
    public async Task<int> Save<T>(T model)
    {
        await Init();
        return await Database.InsertAsync(model);
    }
    public async Task<User?> LoginUser(string user, string password)
    {
        await Init();
        TextEncript encript = new ();
        var userFound = await Database.Table<User>().FirstOrDefaultAsync(p => p.Usuario == user);
        if (userFound == null) return null;
        var clave = encript.DesencriptPassword(userFound.Password);
        if (clave == password) return userFound;
        return null;
    }
    public async Task<User?> LoginUserPin(string pin)
    {
        await Init();
        var user = await Database.Table<User>().FirstOrDefaultAsync(p => p.ClavePin == pin);
        return user;
    }
    public async Task<List<Cuenta>> GetAccountsRecent()
    {
        await Init();
        return await Database.Table<Cuenta>().OrderByDescending(p => p.UltimoAcceso).Take(4).ToListAsync();
    }
    public async Task<List<Cuenta>> GetAccounts(int idCategoria)
    {
        await Init();
        return await Database.Table<Cuenta>().Where(p => p.CategoriaId == idCategoria).ToListAsync();
    }
    public async Task<(int, int)> GetNumberForAccounts()
    {
        await Init();
        var numbersOfWebsite = await Database.Table<Cuenta>().Where(p => p.CategoriaId == 1).CountAsync();
        var numbersOfApps = await Database.Table<Cuenta>().Where(p => p.CategoriaId == 2).CountAsync();
        return (numbersOfWebsite, numbersOfApps);
    }
    public async Task<int> DeleteAccount(Cuenta account)
    {
        await Init();
        return await Database.DeleteAsync(account);
    }
    public async Task<int> UpdateAccount(Cuenta account)
    {
        await Init();
        return await Database.UpdateAsync(account);
    }
}
