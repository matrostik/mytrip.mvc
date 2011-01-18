IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storedepartment_mytrip_storedepartment]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storedepartment]'))
ALTER TABLE [dbo].[mytrip_storedepartment] DROP CONSTRAINT [FK_mytrip_storedepartment_mytrip_storedepartment]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storedepartment_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storedepartment]'))
ALTER TABLE [dbo].[mytrip_storedepartment] DROP CONSTRAINT [FK_mytrip_storedepartment_mytrip_storesale]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storeprofile]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorder]'))
ALTER TABLE [dbo].[mytrip_storeorder] DROP CONSTRAINT [FK_mytrip_storeorder_mytrip_storeprofile]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storemanageorder]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorderisproduct]'))
ALTER TABLE [dbo].[mytrip_storeorderisproduct] DROP CONSTRAINT [FK_mytrip_storeorder_mytrip_storemanageorder]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeorder_mytrip_storeproduct]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeorderisproduct]'))
ALTER TABLE [dbo].[mytrip_storeorderisproduct] DROP CONSTRAINT [FK_mytrip_storeorder_mytrip_storeproduct]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproducer_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproducer]'))
ALTER TABLE [dbo].[mytrip_storeproducer] DROP CONSTRAINT [FK_mytrip_storeproducer_mytrip_storesale]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storedepartment]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct] DROP CONSTRAINT [FK_mytrip_storeproduct_mytrip_storedepartment]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storeproducer]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct] DROP CONSTRAINT [FK_mytrip_storeproduct_mytrip_storeproducer]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storeproduct_mytrip_storesale]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]'))
ALTER TABLE [dbo].[mytrip_storeproduct] DROP CONSTRAINT [FK_mytrip_storeproduct_mytrip_storesale]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_mytrip_storevotes_mytrip_storeproduct]') AND parent_object_id = OBJECT_ID(N'[dbo].[mytrip_storevotes]'))
ALTER TABLE [dbo].[mytrip_storevotes] DROP CONSTRAINT [FK_mytrip_storevotes_mytrip_storeproduct]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeorderisproduct]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storeorderisproduct]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storevotes]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storevotes]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeproduct]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storeproduct]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeproducer]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storeproducer]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storedepartment]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storedepartment]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeorder]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storeorder]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeprofile]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storeprofile]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storesale]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storesale]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mytrip_storeseller]') AND type in (N'U'))
DROP TABLE [dbo].[mytrip_storeseller]