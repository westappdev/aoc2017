import spock.lang.Specification

class Day5Test extends Specification {

    def "calculates the steps in the maze"() {
        when:
        Day5 app = new Day5()

        then:
        app.stepCount([0, 3, 0, 1, -3]) == 5
    }

    def "calculate steps with even more strange logic :)"() {
        when:
        Day5 app = new Day5()

        then:
        app.advancedCount([0, 3, 0, 1, -3]) == 10
    }
}
