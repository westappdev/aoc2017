import spock.lang.Specification

class Day2Test extends Specification {

    String PATH_PART_1 = "src/test/resources/Day2.txt"
    String PATH_PART_2 = "src/test/resources/Day2-Part2.txt"

    def "calculate checksum"() {
        when:
        File file = new File(PATH_PART_1)
        Day2  app = new Day2()

        then:
        app.sChecksum(file) == 18
    }

    def "calculate checksum with division (part 2)"(){
        when:
        File file = new File(PATH_PART_2)
        Day2  app = new Day2()

        then:
        app.sChecksumDivison(file) == 9
    }
}
