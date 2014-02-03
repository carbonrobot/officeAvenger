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
        /// Assigns the avenger to the specified mission. Whether they like it or not.
        /// </summary>
        /// <param name="missionId">The mission identifier.</param>
        /// <param name="avengerId">The avenger identifier.</param>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>ServiceResponse.</returns>
        public ServiceResponse AssignAvenger(int missionId, int avengerId, int agentId)
        {
            Action action = () =>
            {
                var mission = this.Context.AsQueryable<Mission>().Single(x => x.Id == missionId && x.AgentId == agentId);
                var hero = this.Context.AsQueryable<Avenger>().Single(x => x.Id == avengerId && x.AgentId == agentId);

                mission.Team.Add(hero);
                this.Context.Save(mission);
            };
            return this.Execute(action);
        }

        /// <summary>
        /// Begins the mission and starts the mission timer
        /// </summary>
        /// <param name="missionId">The mission id</param>
        /// <param name="agentId">The agent id</param>
        /// <returns></returns>
        public ServiceResponse<Mission> BeginMission(int missionId, int agentId)
        {
            Func<Mission> func = () =>
            {
                var mission = this.Context.AsQueryable<Mission>(m => m.Team).Single(x => x.Id == missionId && x.AgentId == agentId);
                mission.Engage();
                return this.Context.Save(mission);
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets the active missions for this agent
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>ServiceResponse{IList{Mission}}.</returns>
        public ServiceResponse<IList<Mission>> GetActiveMissions(int agentId)
        {
            Func<IList<Mission>> func = () =>
            {
                return this.Context
                    .AsQueryable<Mission>(m => m.Team)
                    .Where(x => x.AgentId == agentId)
                    .ToList();
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Gets the avenger.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>ServiceResponse{Avenger}.</returns>
        public ServiceResponse<Avenger> GetAvenger(int id, int agentId)
        {
            return GetSecureInformation<Avenger>(id, agentId);
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
        /// Gets the mission.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>ServiceResponse{Mission}.</returns>
        public ServiceResponse<Mission> GetMission(int id, int agentId)
        {
            Func<Mission> func = () =>
            {
                var entity = this.Context
                    .AsQueryable<Mission>(m => m.Team)
                    .Single(x => x.Id == id && x.AgentId == agentId);

                return entity;
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Removes the avenger from the specified mission. Take a seat, sucker.
        /// </summary>
        /// <param name="missionId">The mission identifier.</param>
        /// <param name="avengerId">The avenger identifier.</param>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>ServiceResponse.</returns>
        public ServiceResponse RemoveAvenger(int missionId, int avengerId, int agentId)
        {
            Action action = () =>
            {
                var mission = this.Context.AsQueryable<Mission>(m => m.Team).Single(x => x.Id == missionId && x.AgentId == agentId);
                var hero = mission.Team.Single(x => x.Id == avengerId);

                mission.Team.Remove(hero);
                this.Context.Save(mission);
            };
            return this.Execute(action);
        }

        /// <summary>
        /// Updates the avenger or creates a new one if it does not exist
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ServiceResponse{Avenger}.</returns>
        public ServiceResponse<Avenger> UpdateAvenger(Avenger entity, int agentId)
        {
            return UpdateSecureInformation(entity, agentId);
        }

        /// <summary>
        /// Updates the avenger or creates a new one if it does not exist
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>ServiceResponse{Avenger}.</returns>
        public ServiceResponse<Mission> UpdateMission(Mission entity, int agentId)
        {
            return UpdateSecureInformation(entity, agentId);
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

        /// <summary>
        /// Executes a given action while isolating exception handling
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="action">The method to execute</param>
        protected ServiceResponse Execute(Action action)
        {
            var response = new ServiceResponse();
            try
            {
                action.Invoke();
                response.HasError = false;
                response.Exception = null;
            }
            catch (Exception ex)
            {
                this.Log().Error(() => ex.ToString());

                response.HasError = true;
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Checks the security.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="agentId">The agent identifier.</param>
        private void CheckSecurity<T>(T entity, int agentId) where T : AgentOwnedEntity
        {
            CheckSecurity<T>(entity.Id, agentId);
        }

        /// <summary>
        /// Checks the security.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityId">The entity id</param>
        /// <param name="agentId">The agent identifier.</param>
        private void CheckSecurity<T>(int entityId, int agentId) where T : AgentOwnedEntity
        {
            var updateAllowed = this.Context.AsQueryable<T>().Any(x => x.Id == entityId && x.AgentId == agentId);
            if (!updateAllowed)
                throw new ArgumentOutOfRangeException("agentId", "Unauthorized access detected.");
        }

        /// <summary>
        /// Gets the secure information from the server
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>ServiceResponse{``0}.</returns>
        private ServiceResponse<T> GetSecureInformation<T>(int id, int agentId) where T : AgentOwnedEntity
        {
            Func<T> func = () =>
            {
                var entity = this.Context.AsQueryable<T>().Single(x => x.Id == id && x.AgentId == agentId);
                return entity;
            };
            return this.Execute(func);
        }

        /// <summary>
        /// Updates the secure information store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>ServiceResponse{``0}.</returns>
        private ServiceResponse<T> UpdateSecureInformation<T>(T entity, int agentId) where T : AgentOwnedEntity
        {
            Func<T> func = () =>
            {
                if (entity.Id > 0)
                    CheckSecurity(entity, agentId);

                return this.Context.Save(entity);
            };
            return this.Execute(func);
        }

        private IDataContext Context;
    }
}