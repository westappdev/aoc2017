import spock.lang.Specification

/**
 * by imunteanu 
 * 12/5/17
 */
class Day3Test extends Specification {

    def "calculate distance"() {

        setup:
        Day3 app = new Day3()

        expect:
        app.distance(number) == steps

        where:
        number || steps
        1      || 0
        12     || 3
        17     || 4
        13     || 4
        14     || 3
        15     || 2
        19 || 2
        20     || 3
        23     || 2
        25     || 4
        24     || 3
        24     || 3
        1024   || 31
    }

}
