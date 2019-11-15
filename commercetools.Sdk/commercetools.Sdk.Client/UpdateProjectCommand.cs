﻿using commercetools.Sdk.Domain.Projects;

namespace commercetools.Sdk.Client
{
    using System.Collections.Generic;
    using Domain;

    public class UpdateProjectCommand : UpdateCommand<Project>
    {
        public UpdateProjectCommand(int version, List<UpdateAction<Project>> updateActions)
        {
            this.Init(version, updateActions);
        }

        private void Init(int version, List<UpdateAction<Project>> updateActions)
        {
            this.Version = version;
            this.UpdateActions.AddRange(updateActions);
        }
    }
}
