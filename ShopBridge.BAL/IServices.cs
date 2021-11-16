using ShopBridge.Modal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.BAL
{
  public  interface IServices
    {
        public Task<List<InventoryML>> GetALL();
        public Task<InventoryML> GetById(int id);
        public Task<StatusML> Delete(int id);
        public Task<StatusML> Edit(InventoryML inventoryML);
        public Task<StatusML> Create(InventoryML inventoryML);
    }
}
