using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Participation
{
    public int Id { get; set; }
    public int HuntId { get; set; }
    public int CurrentAssignmentId { get; set; }
}

public class ParticipationStore : MonoBehaviour
{
    private static Participation _participation;
    public static Participation Participation
    {
        get => _participation;
        set => _participation = value;
    }
}
