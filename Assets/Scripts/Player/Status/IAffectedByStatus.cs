namespace RogueDescent.Status
{
	public interface IAffectedByStatus
	{
		public void AddStatus(Status status, bool preventDuplicates = true);
		public void RemoveStatus(Status status);
	}
}