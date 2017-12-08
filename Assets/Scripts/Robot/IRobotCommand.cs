using UnityEngine;

public interface IRobotCommand {
    void run();
    void passParameters(params object[] args);
    void executeOn(GameObject gameObject);
    bool isExecuting();
    void update();
    bool executeBetween();
    float executeAfter();
}
