import * as React from 'react';
import { Label } from '@fluentui/react';

/**
 * Represents the props for the InterpolRedNoticeStatusOutput component. Test.
 */
export interface IInterpolRedNoticeStatusOutputProps {
  firstName: string;
  lastName: string;
}

/**
 * Represents the output component for displaying Interpol Red Notice status.
 */
export class InterpolRedNoticeStatusOutput extends React.Component<IInterpolRedNoticeStatusOutputProps, { outputMessage: string }> {
  constructor(props: IInterpolRedNoticeStatusOutputProps) {
    super(props);
    this.state = { outputMessage: "Loading..." };
    this.fetchData(this.props.firstName, this.props.lastName);
  }

  /**
   * Updates the component when the first name or last name props change.
   * @param prevProps - The previous props.
   */
  componentDidUpdate(prevProps: IInterpolRedNoticeStatusOutputProps) {
    if (prevProps.firstName !== this.props.firstName || prevProps.lastName !== this.props.lastName) {
      this.fetchData(this.props.firstName, this.props.lastName);
    }
  }

  /**
   * Fetches data from the Interpol API based on the provided first name and last name.
   * @param firstName - The first name.
   * @param lastName - The last name.
   */
  fetchData(firstName: string, lastName: string) {
    fetch(`https://ws-public.interpol.int/notices/v1/red?name=${firstName}&forename=${lastName}&page=1&resultPerPage=200`)
      .then(response => response.json())
      .then(data => {
        if (data.total === 0) {
          this.setState({ outputMessage: `🟢 No red notice found for ${firstName} ${lastName}` });
        } else {
          this.setState({ outputMessage: `⚠️ ${data.total} red notice(s) found for ${firstName} ${lastName}` });
        }
      });
  }

  render(): React.ReactNode {
    return (
      <Label>
        {this.state.outputMessage}
      </Label>
    )
  }
}