using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Common;
using commercetools.Sdk.Domain.Projects;
using commercetools.Sdk.Domain.Query;

namespace commercetools.Sdk.Client
{
    public static class UpdateCommandExtensions
    {
        public static UpdateCommand<T> Expand<T>(this UpdateCommand<T> command, Expression<Func<T, Reference>> expression)
        {
            command.Expand.Add(new Expansion<T>(expression).ToString());

            return command;
        }

        public static UpdateCommand<T> Expand<T>(this UpdateCommand<T> command, string expression)
        {
            command.Expand.Add(expression);

            return command;
        }

        public static UpdateCommand<T> SetAdditionalParameters<T>(this UpdateCommand<T> command, IAdditionalParameters<T> additionalParameters)
        {
            command.AdditionalParameters = additionalParameters;
            return command;
        }

        public static UpdateByIdCommand<T> UpdateById<T>(this IVersioned<T> resource, Func<List<UpdateAction<T>>, List<UpdateAction<T>>> updateBuilder)
            where T : Resource<T>
        {
            var actions = updateBuilder(new List<UpdateAction<T>>());
            return new UpdateByIdCommand<T>(resource, actions);
        }

        public static UpdateByKeyCommand<T> UpdateByKey<T>(this T resource, Func<List<UpdateAction<T>>, List<UpdateAction<T>>> updateBuilder)
            where T : Resource<T>, IKeyReferencable<T>, IVersioned<T>
        {
            var actions = updateBuilder(new List<UpdateAction<T>>());
            return new UpdateByKeyCommand<T>(resource.Key, resource.Version, actions);
        }

        public static List<UpdateAction<T>> AddUpdate<T>(this List<UpdateAction<T>> updateActions, UpdateAction<T> updateAction)
            where T : Resource<T>
        {
            updateActions.Add(updateAction);
            return updateActions;
        }

        public static List<UpdateAction<T>> ToList<T>(this UpdateAction<T> updateAction)
        {
            var actionsList = new List<UpdateAction<T>>();
            if (updateAction != null)
            {
                actionsList.Add(updateAction);
            }

            return actionsList;
        }

        public static List<UpdateAction<Project>> AddUpdate(this List<UpdateAction<Project>> updateActions, UpdateAction<Project> updateAction)
        {
            updateActions.Add(updateAction);
            return updateActions;
        }

        public static UpdateProjectCommand Update(this Project project, Func<List<UpdateAction<Project>>, List<UpdateAction<Project>>> updateBuilder)
        {
            var actions = updateBuilder(new List<UpdateAction<Project>>());
            return new UpdateProjectCommand(project.Version, actions);
        }
    }
}
