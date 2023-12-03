namespace Dispo.Shared.Core.Domain
{
    public static class RolesManager
    {
        public const string Manager = "manager";
        public const string PurchasingManager = "purchasingManager";
        public const string WarehouseOperator = "warehouseOperator";

        public const string PurchasingManagerAssociated = $"{PurchasingManager},{Manager}";
        public const string WarehouseOperatorAssociated = $"{WarehouseOperator},{Manager}";
        public const string AllRoles = $"{Manager},{WarehouseOperator},{PurchasingManager}";
    }
}