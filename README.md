# API_Project_PM

Warehouse management REST API built with ASP.NET Core 8 + Entity Framework Core + MySQL.

---

## Domains

| Domain | Description |
|---|---|
| `Part` | A part/product stored in the warehouse |
| `Category` | Category grouping parts |
| `Location` | Physical location in warehouse (Zone / Rack / Shelf / Box) |
| `Supplier` | Supplier of parts |
| `PartSupplier` | Junction table — Part ↔ Supplier (many-to-many) |
| `StockItem` | Junction table — Part ↔ Location (many-to-many), holds current quantity |
| `StockMovement` | Immutable audit log of stock IN/OUT movements |

---

## Business Logic

### Part
- SKU must be unique — cannot be changed after creation
- Must belong to a Category
- Can have an optional default Location (set to NULL if location is deleted)
- Soft-deleted only (`IsDeleted` + `DeletedAt`) — never physically removed

### Category
- Name must be unique
- Cannot be deleted if Parts are linked to it

### Location
- Zone + Rack + Shelf + Box combination must be unique
- Two types: `Stock` and `Overstock`
- Soft-deleted only — never physically removed

### Supplier
- VAT number must be unique
- Cannot be deleted if PartSupplier records exist
- Has `IsActive` flag to deactivate without deleting

### PartSupplier
- One supplier cannot be assigned twice to the same part (composite PK: PartId + SupplierId)
- Supplier price is optional
- One supplier per part can be marked as preferred (`IsPreferred`)
- Hidden automatically when Part is soft-deleted

### StockItem
- Quantity must be between 0 and 100 000 (enforced at DB level)
- The same part cannot appear twice in the same location (unique index)
- Hidden automatically when Part or Location is soft-deleted

### StockMovement
- Quantity must be >= 1
- `PUT` and `DELETE` are blocked — history is immutable for audit reasons
- To correct a mistake: add a compensating IN/OUT entry
- `TransferGroupId` groups related movements (e.g. transfers between locations)
- `MovementDate` defaults to `UTC_TIMESTAMP()`

---

## Relations

```
Category ──(Restrict)──< Part >──(SetNull, optional)──> Location
                          │
                          │  many-to-many via PartSupplier
                          ├──< PartSupplier >──> Supplier
                          │
                          │  many-to-many via StockItem
                          ├──< StockItem >──> Location
                          │
                          └──< StockMovement >──> Location
```

### One-to-many

| From | To | On Delete |
|---|---|---|
| Category → Parts | one-to-many | Restrict |
| Location → Parts (default) | one-to-many | SetNull |
| Location → StockMovements | one-to-many | Restrict |
| Part → StockMovements | one-to-many | Restrict |

### Many-to-many

| Junction | Left | Right | Notes |
|---|---|---|---|
| `PartSupplier` | Part | Supplier | optional price, preferred flag |
| `StockItem` | Part | Location | current quantity per location |

---

## Tech Stack

- **ASP.NET Core 8** — Web API + Minimal API
- **Entity Framework Core 8** — ORM
- **MySQL** via `Pomelo.EntityFrameworkCore.MySql`
- **Repository pattern** with interface-based services
- **Soft delete** via `ISoftDeletable` interface + global query filters