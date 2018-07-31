namespace RISAtech.Presentation.R3D.DetailReport.Services
{
    using System;
    using System.Collections.Generic;
    using RISAtech.Application.Contract.DetailReport;
    using RISAtech.Contract.R3D.Enumerations;
    using RISAtech.Contract.R3D.Metadata;
    using RISAtech.Infrastructure.Container;
    using RISAtech.Infrastructure.Logger;
    using RISAtech.Presentation.Common.DataAnnotation;
    using RISAtech.Presentation.R3D.Core;
    using RISAtech.Presentation.R3D.DetailReport.ViewModels;
    using RISAtech.Presentation.R3D.DetailReport.Views;
    using RISAtech.Presentation.WPF.Services;

    public class DetailReportUIService : IDetailReportUIService
    {
        public DetailReportUIService(
            ILogger log,
            IResolver resolver,
            IDialogUIService dialogUIService,
            IDetailReportService detailReportService)
        {
            Log = log;
            Resolver = resolver;
            DialogUIService = dialogUIService;
            DetailReportService = detailReportService;
        }

        protected ILogger Log { get; }

        protected IResolver Resolver { get; }

        protected IDialogUIService DialogUIService { get; }

        protected IDetailReportService DetailReportService { get; }

        public DetailReportOptionsDTO ShowDetailReportOptions(DetailReportOptionsDTO context)
        {
            if (DialogUIService.Show<IDetailReportOptionsDialog, IDetailReportOptionsDialogViewModel, DetailReportOptionsDTO>(context))
            {
                return context;
            }

            return context;
        }

        public IDictionary<string, string> GetMaterialProperties(MaterialTypes materialType, Guid materialId)
        {
            var dictionary = new Dictionary<string, string>();
            var holder = Resolver.Resolve<IRisa3DMetadataHolder>();
            GridCodes gridCode = GridCodes.NONE;
            switch (materialType)
            {
                case MaterialTypes.HOT_ROLLED_STEEL_MATL:
                    gridCode = GridCodes.HRSTL_GRID_NUMBER;
                    break;

                case MaterialTypes.COLD_FORMED_STEEL_MATL:
                    gridCode = GridCodes.CFSTL_GRID_NUMBER;
                    break;
                case MaterialTypes.ALUMINUM_MATL:
                    gridCode = GridCodes.AL_MATL_GRID_NUMBER;
                    break;

                case MaterialTypes.CONCRETE_MATL:
                    gridCode = GridCodes.CONC_MATL_GRID_NUMBER;
                    break;

                case MaterialTypes.NDS_WOOD_MATL:
                    gridCode = GridCodes.WOOD_PROP_GRID_NUMBER;
                    break;

                case MaterialTypes.STAINLESS_STEEL_MATL:
                    gridCode = GridCodes.SS_GRID_NUMBER;
                    break;

                case MaterialTypes.GENERAL_MATL:
                    gridCode = GridCodes.MATL_GRID_NUMBER;
                    break;
            }

            var propertyInfoHolders = holder.GetGridProperty(Risa3DTypeKind.SpreadsheetModel, gridCode);
            foreach (var propertyInfo in propertyInfoHolders)
            {
                if (propertyInfo.ColumnIndex >= 0)
                {
                    var name = (propertyInfo.GetAttribute(typeof(PropertyTitleAttribute)) as PropertyTitleAttribute)?.Title;
                    if (propertyInfo.Name == "Label")
                    {
                        name = "Material";
                    }

                    var units = string.Empty;
                    if (propertyInfo.HasAttribute(typeof(UnitLabelAttribute)))
                    {
                        units = (propertyInfo.GetAttribute(typeof(UnitLabelAttribute)) as UnitLabelAttribute)?.TitleSuffix;
                    }
                    else if (propertyInfo.HasAttribute(typeof(CompositeUnitLabelAttribute)))
                    {
                        units = (propertyInfo.GetAttribute(typeof(CompositeUnitLabelAttribute)) as CompositeUnitLabelAttribute)?.TitleSuffix;
                    }

                    if (!string.IsNullOrEmpty(units))
                    {
                        units = $" ({units})";
                    }

                    var value = " "; // DetailReportService.GetMaterialValues(materialId, gridCode, propertyInfo.ColumnIndex);

                    dictionary.Add($"{name}{units}:", value);
                }
            }

            return dictionary;
        }
    }
}
