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
    private static string _huntTitle;
    public static string HuntTitle
    {
        get => _huntTitle;
        set => _huntTitle = value;
    }

    private static Assignment _assignment;
    public static Assignment Assignment
    {
        get => _assignment;
        set => _assignment = value;
    }

}
