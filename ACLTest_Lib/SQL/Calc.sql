declare @dFromLat float=0.496197106341988
declare @dFromLong float=-1.41909812597572
declare @dToLat float=0.450207680551937
declare @dToLong float=-1.40132485642625
declare @rInMiles float=3956.4895272
declare @rInNMiles float=3438.034

declare @calcInMiles float=(Acos(Cos(@dFromLat) * Cos(@dFromLong) * Cos(@dToLat) * Cos(@dToLong) + Cos(@dFromLat) * Sin(@dFromLong) * Cos(@dToLat) * Sin(@dToLong) + Sin(@dFromLat) * Sin(@dToLat)) * @rInMiles)
declare @calcInNMiles float=(Acos(Cos(@dFromLat) * Cos(@dFromLong) * Cos(@dToLat) * Cos(@dToLong) + Cos(@dFromLat) * Sin(@dFromLong) * Cos(@dToLat) * Sin(@dToLong) + Sin(@dFromLat) * Sin(@dToLat)) * @rInNMiles)

select cast(@calcInMiles as decimal(20,12)) 'Miles', cast(@calcInNMiles as decimal(20,12)) 'Nautical Miles'