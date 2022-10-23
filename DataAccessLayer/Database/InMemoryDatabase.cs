using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Database
{
	public class InMemoryDatabase<T> : IDbWrapper<T> where T : DataEntity
	{
		private Dictionary<Tuple<string, string>, DataEntity> _databaseInstance;

		public InMemoryDatabase()
		{
			_databaseInstance = new Dictionary<Tuple<string, string>, DataEntity>();
			
			// added for test purpose, can be found at the bottom of the page
			InsertTestData();
		}

		public bool Insert(T data)
		{
			try
			{
				_databaseInstance.Add(Tuple.Create(data.SiteId, data.CompanyCode), data);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Update(T data)
		{
			try
			{
				if (_databaseInstance.ContainsKey(Tuple.Create(data.SiteId, data.CompanyCode)))
				{
					_databaseInstance.Remove(Tuple.Create(data.SiteId, data.CompanyCode));
					Insert(data);
					return true;
				}

				return false;
			}
			catch
			{
				return false;
			}
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
		{
			try
			{
				var entities = FindAll();
				return entities.Where(expression.Compile());
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		public IEnumerable<T> FindAll()
		{
			try
			{
				return _databaseInstance.Values.OfType<T>();
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		public bool Delete(Expression<Func<T, bool>> expression)
		{
			try
			{
				var entities = FindAll();
				var entity = entities.Where(expression.Compile());
				foreach (var dataEntity in entity)
				{
					_databaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
				}
				
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool DeleteAll()
		{
			try
			{
				_databaseInstance.Clear();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateAll(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
		{
			try
			{
				var entities = FindAll();
				var entity = entities.Where(filter.Compile());
				foreach (var dataEntity in entity)
				{
					var newEntity = UpdateProperty(dataEntity, fieldToUpdate, newValue);

					_databaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
					Insert(newEntity);
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		private T UpdateProperty(T dataEntity, string key, object value)
		{
			Type t = typeof(T);
			var prop = t.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (prop == null)
			{
				throw new Exception("Property not found");
			}

			prop.SetValue(dataEntity, value, null);
			return dataEntity;
		}

		public Task<bool> InsertAsync(T data)
		{
			return Task.FromResult(Insert(data));
		}

		public Task<bool> UpdateAsync(T data)
		{
			return Task.FromResult(Update(data));
		}

		public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
		{
			return Task.FromResult(Find(expression));
		}

		public Task<IEnumerable<T>> FindAllAsync()
		{
			return Task.FromResult(FindAll());
		}

		public Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
		{
			return Task.FromResult(Delete(expression));
		}

		public Task<bool> DeleteAllAsync()
		{
			return Task.FromResult(DeleteAll());
		}

		public Task<bool> UpdateAllAsync(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
		{
			return Task.FromResult(UpdateAll(filter, fieldToUpdate, newValue));
		}

		/// <summary>
		/// Added for test purpose
		/// </summary>
		public void InsertTestData()
		{
			Insert(new Company()
			{
				SiteId = "1",
				CompanyCode = "4",
				CompanyName = "Rogers"
			} as T);
			Insert(new Company()
			{
				SiteId = "2",
				CompanyCode = "5",
				CompanyName = "TD"
			} as T);
			Insert(new Company()
			{
				SiteId = "3",
				CompanyCode = "15",
				CompanyName = "Jonas"
			} as T);

			Insert(new Employee()
			{
				SiteId = "10",
				CompanyCode = "15",
				EmployeeCode = "80",
				EmployeeName = "Yalin",
				Occupation = "MSc Computer Science",
				Phone = "(123) 456 78 90",
				LastModified = DateTime.Now
			} as T);
			
			Insert(new Employee()
			{
				SiteId = "11",
				CompanyCode = "15",
				EmployeeCode = "81",
				EmployeeName = "Aydin",
				Occupation = "BEng",
				Phone = "(901) 123 45 67",
				LastModified = DateTime.Now
			} as T);
		}
	
	}
}
