import * as React from 'react';
import { Label } from '@fluentui/react';

export interface IInterpolRedNoticeStatusOutputProps {
  firstName: string;
  lastName: string;
}

export class InterpolRedNoticeStatusOutput extends React.Component<IInterpolRedNoticeStatusOutputProps, { outputMessage: string }> {
  constructor(props: IInterpolRedNoticeStatusOutputProps) {
    super(props);
    this.state = { outputMessage: "Loading..." };
    this.fetchData(this.props.firstName, this.props.lastName);
  }

  componentDidUpdate(prevProps: IInterpolRedNoticeStatusOutputProps) {
    if (prevProps.firstName !== this.props.firstName || prevProps.lastName !== this.props.lastName) {
      this.fetchData(this.props.firstName, this.props.lastName);
    }
  }

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