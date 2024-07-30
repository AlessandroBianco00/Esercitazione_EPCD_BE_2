namespace PizzeriaWebApp.Interfaces
{
    public interface IGenericService<T> where T : class 
    {
        public T Create(T entity);
        public T Update(T entity);
        public T Delete(T entity);
        //public T Get(int id); Get può avere parametri diversi (id, nome), non lo implemento
        public IEnumerable<T> GetAll();

    }
}
