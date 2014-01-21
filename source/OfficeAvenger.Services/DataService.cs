using System;
using System.Collections.Generic;
using System.Linq;
using OfficeAvenger.Domain;
using OfficeAvenger.Services.Logging;

namespace OfficeAvenger.Services
{
    public class DataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class.
        /// </summary>
        /// <param name="context">The repository data context.</param>
        public DataService(IDataContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets the avenger.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ServiceResponse{Avenger}.</returns>
        public ServiceResponse<Avenger> GetAvenger(int id, int agentId)
        {
            Func<Avenger> func = () =>
            {
                var entity = this.Context.AsQueryable<Avenger>().Single(x => x.Id == id && x.AgentId == agentId);
                return entity;
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets the list of avengers
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ServiceResponse{Avenger}.</returns>
        public ServiceResponse<IList<Avenger>> GetAvengers(int agentId)
        {
            Func<IList<Avenger>> func = () =>
            {
                return this.Context.AsQueryable<Avenger>().Where(x => x.AgentId == agentId).ToList();
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Updates the avenger or creates a new one if it does not exist
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ServiceResponse{Avenger}.</returns>
        public ServiceResponse<Avenger> UpdateAvenger(Avenger entity, int agentId)
        {
            Func<Avenger> func = () =>
            {
                // if this is an update, verify security first
                if (entity.Id > 0)
                {
                    var updateAllowed = this.Context.AsQueryable<Avenger>().Any(x => x.Id == entity.Id && x.AgentId == agentId);
                    if (updateAllowed)
                    {
                        return this.Context.Save(entity);
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("agentId", "Unauthorized access detected.");
                    }
                }
                else
                {
                    return this.Context.Save(entity);
                }
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Executes a given function while isolating exception handling
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="func">The method to execute</param>
        protected ServiceResponse<T> Execute<T>(Func<T> func)
        {
            var response = new ServiceResponse<T>();
            try
            {
                response.Result = func.Invoke();
                response.HasError = false;
                response.Exception = null;
            }
            catch (Exception ex)
            {
                this.Log().Error(() => ex.ToString());

                response.Result = default(T);
                response.HasError = true;
                response.Exception = ex;
            }
            return response;
        }

        private IDataContext Context;
    }
}